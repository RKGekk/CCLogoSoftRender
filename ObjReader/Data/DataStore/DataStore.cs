﻿using ObjReader.Common;
using ObjReader.Data.Elements;
using ObjReader.Data.VertexData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.Data.DataStore {
    public class DataStore : IDataStore, IGroupDataStore, IObjectDataStore, IVertexDataStore, ITextureDataStore, INormalDataStore, IFaceGroup, IMaterialLibrary, IElementGroup {

        private Group _currentGroup;
        private ObjectContainer _currentObject;

        private readonly List<Group> _groups = new List<Group>();
        private readonly List<ObjectContainer> _objects = new List<ObjectContainer>();
        private readonly List<Material> _materials = new List<Material>();

        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<Normal> _normals = new List<Normal>();

        public IList<Vertex> Vertices {
            get { return _vertices; }
        }

        public IList<Texture> Textures {
            get { return _textures; }
        }

        public IList<Normal> Normals {
            get { return _normals; }
        }

        public IList<Material> Materials {
            get { return _materials; }
        }

        public IList<Group> Groups {
            get { return _groups; }
        }

        public IList<ObjectContainer> Objects {
            get { return _objects; }
        }

        public void AddFace(Face face) {
            PushGroupIfNeeded();

            _currentGroup.AddFace(face);
        }

        public void PushGroup(string groupName) {
            _currentGroup = new Group(groupName);
            _groups.Add(_currentGroup);
        }

        private void PushGroupIfNeeded() {
            if (_currentGroup == null) {
                PushGroup("default");
            }
        }

        public void PushObject(string objectName) {
            _currentObject = new ObjectContainer(objectName);
            _objects.Add(_currentObject);
        }

        private void PushObjectIfNeeded() {
            if (_currentObject == null) {
                PushObject("default");
            }
        }

        public void AddVertex(Vertex vertex) {
            _vertices.Add(vertex);
        }

        public void AddTexture(Texture texture) {
            _textures.Add(texture);
        }

        public void AddNormal(Normal normal) {
            _normals.Add(normal);
        }

        public void Push(Material material) {
            _materials.Add(material);
        }

        public void SetMaterial(string materialName) {

            var material = _materials.SingleOrDefault(x => x.Name.EqualsOrdinalIgnoreCase(materialName));
            PushGroupIfNeeded();
            _currentGroup.Material = material;
        }
    }
}
