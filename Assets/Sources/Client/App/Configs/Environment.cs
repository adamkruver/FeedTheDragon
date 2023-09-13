﻿using System;
using System.Collections.Generic;

namespace Sources.Client.App.Configs
{
    [Serializable]
    public class Environment
    {
        public Dictionary<string, string> View;
        public Dictionary<string, string> Ui;
    }
}