using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piko.App;
using UnityEngine.UI;

namespace Piko.App.Controllers
{
    [System.Serializable]
    public class ExploreModel : Model
    {
        [SerializeField]
        private float _distance;
        private ExploreController _controller;

        public void SetMapDistance(float distance, ExploreController controller)
        {
            this._distance = distance;
            this._controller = controller;
            ProcessDistance();
        }

        void ProcessDistance()
        {
            //perform map caluclation based on distance
            Debug.Log("Distance updated to: " + _distance);
            _controller.OnMapDistanceUpdated();
        }

        public void ProcessColor(ExploreController controller)
        {
            this._controller = controller;
            var newColor = Random.ColorHSV();
            _controller.TurnButtonRandomColorComplete(newColor);
        }
    }

    [System.Serializable]
    public class ExploreView : View
    {
        [SerializeField]
        private Button _mapsButton;
        [SerializeField]
        private Slider _distanceSlider;
        [SerializeField]
        private Image _img;

        public float GetDistanceValue() => _distanceSlider.value;

        public void SetColor(Color newColor)
        {
            _img.color = newColor;
        }
    }

    public class ExploreController : Controller<ExploreModel, ExploreView>
    {

        protected override void Enabled()
        {
            base.Enabled();
            //this automatically gets called when a panel becomes active
        }

        public void UpdateMapDistance()
        {
            model.SetMapDistance(view.GetDistanceValue(), this);
        }

        public void OnMapDistanceUpdated()
        {
            Debug.Log("Map has finished updating with new distance");
        }

        public void TurnButtonRandomColor()
        {
            model.ProcessColor(this);
        }

        public void TurnButtonRandomColorComplete(Color newColor)
        {
            view.SetColor(newColor);
        }

        protected override void Disabled()
        {
            base.Disabled();
            //automatically called when this panel is disabled 
        }

    }
}

