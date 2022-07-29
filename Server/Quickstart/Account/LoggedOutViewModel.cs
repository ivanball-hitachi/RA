// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace IdentityServerHost.Quickstart.UI
{
    public class LoggedOutViewModel
    {
        public string PostLogoutRedirectUri { get; set; } = default!;
        public string ClientName { get; set; } = default!;
        public string SignOutIframeUrl { get; set; } = default!;

        public bool AutomaticRedirectAfterSignOut { get; set; }

        public string LogoutId { get; set; } = default!;
        public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;
        public string ExternalAuthenticationScheme { get; set; } = default!;
    }
}