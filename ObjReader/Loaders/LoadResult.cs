﻿using ObjReader.Data;
using ObjReader.Data.Elements;
using ObjReader.Data.VertexData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.Loaders {

    public class LoadResult {

        public IList<Vertex> Vertices { get; set; }
        public IList<Texture> Textures { get; set; }
        public IList<Normal> Normals { get; set; }
        public IList<Group> Groups { get; set; }
        public IList<ObjectContainer> Objects { get; set; }
        public IList<Material> Materials { get; set; }
    }
}
