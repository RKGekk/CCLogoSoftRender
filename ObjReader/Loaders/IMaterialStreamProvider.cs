﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjReader.Loaders {

    public interface IMaterialStreamProvider {

        Stream Open(string materialFilePath);
    }
}
