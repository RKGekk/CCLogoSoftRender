using MathematicalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRender.Engine {

    public class RObject {

        public int id;
        public string name;

        public int num_polys() {
            return polygons.Count;
        }

        public int num_frames;
        public int curr_frame;

        public float avgRadius;
        public float maxRadius;

        public RTexture texture;

        public Vec3f pos;
        public Vec3f dir;
        public Vec3f ux;
        public Vec3f uy;
        public Vec3f uz;

        public Mat4f model;
        public Mat4f world;
        public Mat4f view;
        public Mat4f proj;

        public List<RPolygon> polygons = new List<RPolygon>();
        public List<RPolygon> polygons_raster = new List<RPolygon>();

        public bool visible;

        public void calculateAll(float nearPlane, float farPlane, float width, float height, bool isNDC, bool backFaceCull = true) {

            polygons_raster = new List<RPolygon>();
            object sync = new object();
            Parallel.ForEach<RPolygon>(
                polygons,
                poly => {
                    Mat4f mwv = model;
                    mwv = mwv * world;
                    mwv = mwv * view;

                    RVertex vertex1 = new RVertex(poly.vertex0);
                    vertex1.position = new Vec3f(mwv * poly.v0);

                    RVertex vertex2 = new RVertex(poly.vertex1);
                    vertex2.position = new Vec3f(mwv * poly.v1);

                    RVertex vertex3 = new RVertex(poly.vertex2);
                    vertex3.position = new Vec3f(mwv * poly.v2);

                    Vec3f nearPlaneNormal = new Vec3f(0.0f, 0.0f, 1.0f);
                    Vec3f nearPlaneP0 = new Vec3f(0.0f, 0.0f, nearPlane);

                    Vec3f farPlaneNormal = new Vec3f(0.0f, 0.0f, -1.0f);
                    Vec3f farPlaneP0 = new Vec3f(0.0f, 0.0f, farPlane);

                    float sideOfPoint1near = nearPlaneNormal.dot(vertex1.position - nearPlaneP0);
                    float sideOfPoint2near = nearPlaneNormal.dot(vertex2.position - nearPlaneP0);
                    float sideOfPoint3near = nearPlaneNormal.dot(vertex3.position - nearPlaneP0);

                    float sideOfPoint1far = farPlaneNormal.dot(vertex1.position - farPlaneP0);
                    float sideOfPoint2far = farPlaneNormal.dot(vertex2.position - farPlaneP0);
                    float sideOfPoint3far = farPlaneNormal.dot(vertex3.position - farPlaneP0);

                    if ((sideOfPoint1near >= 0 && sideOfPoint2near >= 0 && sideOfPoint3near >= 0) && (sideOfPoint1far >= 0 && sideOfPoint2far >= 0 && sideOfPoint3far >= 0)) {

                        vertex1.position = new Vec3f(proj * vertex1.position);
                        vertex2.position = new Vec3f(proj * vertex2.position);
                        vertex3.position = new Vec3f(proj * vertex3.position);

                        vertex1.position.x = isNDC ? (vertex1.position.x + 1.0f) * 0.5f * width : vertex1.position.x * width;
                        vertex1.position.y = isNDC ? (vertex1.position.y + 1.0f) * 0.5f * height : vertex1.position.y * height;

                        vertex2.position.x = isNDC ? (vertex2.position.x + 1.0f) * 0.5f * width : vertex2.position.x * width;
                        vertex2.position.y = isNDC ? (vertex2.position.y + 1.0f) * 0.5f * height : vertex2.position.y * height;

                        vertex3.position.x = isNDC ? (vertex3.position.x + 1.0f) * 0.5f * width : vertex3.position.x * width;
                        vertex3.position.y = isNDC ? (vertex3.position.y + 1.0f) * 0.5f * height : vertex3.position.y * height;

                        RPolygon newPolygon = new RPolygon(vertex1, vertex2, vertex3, poly.rasterType, poly.texture);

                        float cosTheta = newPolygon.normalCalc().dot(GlobalDirection.forward3f);
                        newPolygon.visible = cosTheta >= 0.0f ? false : true;

                        if (!backFaceCull || newPolygon.visible) {
                            newPolygon.sort();
                            lock (sync) {
                                polygons_raster.Add(newPolygon);
                            }
                        }

                    }
                }
            );
        }
    }
}
