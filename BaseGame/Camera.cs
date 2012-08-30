using Microsoft.Xna.Framework;

namespace BaseGame
{
    public class Camera
    {
#region Properties
        private Matrix _camperaProjectionMatrix;
        public Matrix CamperaProjectionMatrix
        {
            get { return _camperaProjectionMatrix; }
            set { _camperaProjectionMatrix = value;
                _bindingFrustum = null;
            }
        }
        
        private Matrix _cameraViewMatrix;
        public Matrix CameraViewMatrix
        {
            get
            {
                return _cameraViewMatrix;

            }
        }

        private Vector3 _cameraPosition;
        public Vector3 CameraPosition
        {
            get { return _cameraPosition; }
            set
            {
                _cameraPosition = value;
                setCameraViewMatrix();
            }
        }

        private Vector3 _cameraLookAt; 
        public Vector3 CameraLookAt
        {
            get { return _cameraLookAt; }
            set
            {
                _cameraLookAt = value;
                setCameraViewMatrix();
            }
        }

#endregion

        public Camera(Vector3 _cameraposition, Vector3 _cameralookat, Matrix _camperaProjectionMatrix)
        {
            CameraPosition = _cameraposition;
            CameraLookAt = _cameralookat;
            CamperaProjectionMatrix = _camperaProjectionMatrix;
        }
        private void setCameraViewMatrix()
        {
            _cameraViewMatrix = Matrix.CreateLookAt(_cameraPosition, _cameraLookAt, Vector3.Up);
            _bindingFrustum = null;
        }

        private BoundingFrustum _bindingFrustum = null;
        public BoundingFrustum BindingFrustum 
        {
            get
            {
                if(_bindingFrustum==null)
                {
                    _bindingFrustum = new BoundingFrustum(_cameraViewMatrix * _camperaProjectionMatrix);
                }
                return _bindingFrustum;
            }
        }


    }
}
