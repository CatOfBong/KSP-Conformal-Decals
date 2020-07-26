using System;
using UnityEngine.Events;

namespace ConformalDecals.Text {
    public class TextRenderJob {
        public DecalText OldText { get; }
        public DecalText NewText { get; }
        public bool Needed { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsDone { get; private set; }

        public readonly TextRenderer.TextRenderEvent onRenderFinished = new TextRenderer.TextRenderEvent();
        
        public TextRenderJob(DecalText oldText, DecalText newText, UnityAction<RenderedText> renderFinishedCallback) {
            OldText = oldText ?? throw new ArgumentNullException(nameof(oldText));
            NewText = newText ?? throw new ArgumentNullException(nameof(newText));
            Needed = true;
            
            if (renderFinishedCallback != null) onRenderFinished.AddListener(renderFinishedCallback);
        }
        
        public TextRenderJob( DecalText newText, UnityAction<RenderedText> renderFinishedCallback) {
            NewText = newText ?? throw new ArgumentNullException(nameof(newText));
            Needed = true;
            
            if (renderFinishedCallback != null) onRenderFinished.AddListener(renderFinishedCallback);
        }

        public void Cancel() {
            Needed = false;
        }

        public void Start() {
            IsStarted = true;
        }

        public void Finish(RenderedText output) {
            IsDone = true;
            onRenderFinished.Invoke(output);
        }
    }
}