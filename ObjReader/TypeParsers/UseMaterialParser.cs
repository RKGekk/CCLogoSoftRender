﻿using ObjReader.Data.DataStore;
using ObjReader.TypeParsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.TypeParsers {

    public class UseMaterialParser : TypeParserBase, IUseMaterialParser {

        private readonly IElementGroup _elementGroup;

        public UseMaterialParser(IElementGroup elementGroup) {

            _elementGroup = elementGroup;
        }

        protected override string Keyword {
            get { return "usemtl"; }
        }

        public override void Parse(string line) {
            _elementGroup.SetMaterial(line);
        }
    }
}
