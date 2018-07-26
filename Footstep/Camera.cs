using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep
{
    class Camera
    {
        public Camera(GraphicsDevice gd) {
            graphicsDevice = gd;
        }

        GraphicsDevice graphicsDevice;

        Vector3 position = new Vector3(0.1f, 0f, 2);

        public Matrix ViewMatrix {
            get {
                var lookAtVector = Vector3.Zero;
                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(position, lookAtVector, upVector);
            }
        }
        public Matrix ProjectionMatrix {
            get {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 400;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }
        public void moveOut() {
            position.Z += 0.1f;
        }
        public void moveIn() {
            position.Z -= 0.1f;
        }
        public void moveLeft() {
            position.X += 0.1f;
        }
        public void moveRight() {
            position.X -= 0.1f;
        }
        public void moveUp() {
            position.Y -= 0.1f;
        }
        public void moveDown() {
            position.Y += 0.1f;
        }
        public void Update(GameTime gameTime) {

        }
    }
}
