﻿using ObjReader.Common;
using ObjReader.Data.DataStore;
using ObjReader.Data.VertexData;
using ObjReader.TypeParsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.TypeParsers {

    public class NormalParser : TypeParserBase, INormalParser {

        private readonly INormalDataStore _normalDataStore;

        public NormalParser(INormalDataStore normalDataStore) {
            _normalDataStore = normalDataStore;
        }

        protected override string Keyword {
            get { return "vn"; }
        }

        public override void Parse(string line) {
            string[] parts = line.Split(' ');

            float x = parts[0].ParseInvariantFloat();
            float y = parts[1].ParseInvariantFloat();
            float z = parts[2].ParseInvariantFloat();

            var normal = new Normal(x, y, z);
            _normalDataStore.AddNormal(normal);
        }
    }
}
