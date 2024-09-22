using UnityEngine;

namespace Contents.Scene
{
    public class MiniBossScene: BaseScene
    {
        public Transform _leftPoint;
        public Transform _rightPoint;
        
        protected override void Init()
        {
            base.Init();
            SceneType = Define.Scene.CaveScene;
            Managers.UI.ShowSceneUI<UI_Hud>();
        }
        public override void Clear()
        {
        }
    }
}