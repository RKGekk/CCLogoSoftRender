﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.TypeParsers.Interfaces {

    public interface ITypeParser {

        bool CanParse(string keyword);
        void Parse(string line);
    }
}
