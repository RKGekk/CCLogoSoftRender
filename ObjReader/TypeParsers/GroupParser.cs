﻿using ObjReader.Data.DataStore;
using ObjReader.TypeParsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.TypeParsers {

    public class GroupParser : TypeParserBase, IGroupParser {

        private readonly IGroupDataStore _groupDataStore;

        public GroupParser(IGroupDataStore groupDataStore) {
            _groupDataStore = groupDataStore;
        }

        protected override string Keyword {
            get { return "g"; }
        }

        public override void Parse(string line) {
            _groupDataStore.PushGroup(line);
        }
    }
}
