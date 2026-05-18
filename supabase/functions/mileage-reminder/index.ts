import { createClient } from 'https://esm.sh/@supabase/supabase-js@2'
import { Resend } from 'https://esm.sh/resend'

const supabase = createClient(
  Deno.env.get('SUPABASE_URL')!,
  Deno.env.get('SUPABASE_SERVICE_ROLE_KEY')!
)

const resend = new Resend(Deno.env.get('RESEND_API_KEY')!)

Deno.serve(async () => {
  const { data: vehicles } = await supabase
    .from('vehicles')
    .select('*')
    .eq('status', 'Active')

  if (!vehicles?.length) return new Response('No active vehicles', { status: 200 })

  for (const vehicle of vehicles) {
    if (!vehicle.last_driver) continue

    const { data: driver } = await supabase
      .from('drivers')
      .select('email, name')
      .eq('name', vehicle.last_driver)
      .single()

    if (!driver?.email) continue

    await resend.emails.send({
      from: 'Fleet Manager <noreply@yourdomain.com>',
      to: driver.email,
      subject: `Mileage Update Required — ${vehicle.registration}`,
      html: `
        <div style="font-family: sans-serif; max-width: 480px; margin: auto; padding: 24px;">
          <h2 style="color: #1d4ed8;">🚗 Mileage Update Reminder</h2>
          <p>Hi <strong>${driver.name}</strong>,</p>
          <p>Please update the current mileage for your assigned vehicle:</p>
          <div style="background: #f1f5f9; border-radius: 8px; padding: 16px; margin: 16px 0;">
            <strong>${vehicle.registration}</strong> — ${vehicle.make} ${vehicle.model}<br/>
            <span style="color: #64748b;">Last recorded: ${vehicle.mileage} km</span>
          </div>
          <a href="${Deno.env.get('APP_URL')}/driver?action=update-mileage&vehicle=${vehicle.id}"
             style="display:inline-block; background:#1d4ed8; color:#fff; padding:12px 24px;
                    border-radius:8px; text-decoration:none; font-weight:600; margin-top:8px;">
            Update Mileage Now
          </a>
          <p style="color:#94a3b8; font-size:12px; margin-top:24px;">
            This reminder is sent every 12 hours while a vehicle is assigned to you.
          </p>
        </div>
      `
    })
  }

  return new Response(`Reminders sent to ${vehicles.length} driver(s)`, { status: 200 })
})