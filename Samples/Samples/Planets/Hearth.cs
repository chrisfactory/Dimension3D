﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimension3D.Samples
{
    public class Hearth : Planet
    {
        public Hearth()
        {
            Items.Add(new Moon());
        }
    }
}