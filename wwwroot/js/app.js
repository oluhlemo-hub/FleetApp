window.positionCoachMark = function (elementId, position) {
    const el        = document.getElementById(elementId);
    const card      = document.getElementById('coach-card');
    const highlight = document.getElementById('coach-highlight');
    if (!card || !highlight) return;
    if (!el) {
        card.style.top       = '50%';
        card.style.left      = '50%';
        card.style.transform = 'translate(-50%, -50%)';
        highlight.style.display = 'none';
        return;
    }
    card.style.transform    = '';
    highlight.style.display = 'block';
    const rect  = el.getBoundingClientRect();
    const pad   = 8;
    const gap   = 12;
    const cardW = 300;
    const cardH = 200;
    const vw    = window.innerWidth;
    const vh    = window.innerHeight;
    highlight.style.left   = (rect.left   - pad) + 'px';
    highlight.style.top    = (rect.top    - pad) + 'px';
    highlight.style.width  = (rect.width  + pad * 2) + 'px';
    highlight.style.height = (rect.height + pad * 2) + 'px';
    let top, left;
    if      (position === 'bottom') { top = rect.bottom + pad + gap;               left = rect.left + rect.width / 2 - cardW / 2; }
    else if (position === 'top')    { top = rect.top - pad - gap - cardH;           left = rect.left + rect.width / 2 - cardW / 2; }
    else if (position === 'right')  { top = rect.top + rect.height / 2 - cardH / 2; left = rect.right + pad + gap; }
    else                            { top = rect.top + rect.height / 2 - cardH / 2; left = rect.left - pad - gap - cardW; }
    left = Math.max(8, Math.min(left, vw - cardW - 8));
    top  = Math.max(8, Math.min(top,  vh - cardH - 8));
    card.style.top  = top  + 'px';
    card.style.left = left + 'px';
    el.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
};

window.triggerClick = function (elementId) {
    const el = document.getElementById(elementId);
    if (el) el.click();
};
