#!/bin/bash

echo "👀 Watching for changes... (Ctrl+C to stop)"
echo "📁 Project: $(pwd)"
echo ""

while true; do
    CHANGES=$(git status --porcelain)

    if [ -n "$CHANGES" ]; then
        echo ""
        echo "📝 Changes detected:"
        git status --short
        echo ""

        CHANGED_FILES=$(git status --porcelain | awk '{print $2}' | tr '\n' ', ' | sed 's/,$//')
        TIMESTAMP=$(date '+%Y-%m-%d %H:%M')
        COMMIT_MSG="Auto-deploy: updated $CHANGED_FILES [$TIMESTAMP]"

        echo "🚀 Committing and deploying..."
        git add .
        git commit -m "$COMMIT_MSG"
        git push origin main

        if [ $? -eq 0 ]; then
            echo ""
            echo "✅ Deployed successfully at $TIMESTAMP"
        else
            echo ""
            echo "❌ Deploy failed. Check the error above."
        fi

        echo ""
        echo "👀 Watching for more changes..."
    fi

    sleep 10
done
