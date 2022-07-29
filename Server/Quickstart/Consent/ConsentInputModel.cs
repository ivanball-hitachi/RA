// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace IdentityServerHost.Quickstart.UI
{
    public class ConsentInputModel
    {
        public string Button { get; set; } = default!;
        public IEnumerable<string> ScopesConsented { get; set; } = default!;
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}