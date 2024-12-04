using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ArtifactSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<ArtifactSplitView, TwoPaneSplitView.UxmlTraits> { };
    }
}