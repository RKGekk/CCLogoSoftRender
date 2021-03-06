﻿using ObjReader.Loaders;
using ObjReader.TypeParsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.TypeParsers {

    public class MaterialLibraryParser : TypeParserBase, IMaterialLibraryParser {

        private readonly IMaterialLibraryLoaderFacade _libraryLoaderFacade;

        public MaterialLibraryParser(IMaterialLibraryLoaderFacade libraryLoaderFacade) {
            _libraryLoaderFacade = libraryLoaderFacade;
        }

        protected override string Keyword {
            get { return "mtllib"; }
        }

        public override void Parse(string line) {
            _libraryLoaderFacade.Load(line);
        }
    }
}
