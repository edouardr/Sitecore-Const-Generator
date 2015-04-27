
namespace SitecoreConstGenerator
{
    public static class FieldIds
    {
        public class Base
        {


        }

        public class BaseNavigation
        {
            #region PageNavigation
            public const string HideFromTopNavigation = @"{24A2D7FA-71A8-4AF7-B285-F4A658DDBF5A}";
            public const string TopNavigationTitle = @"{BB1061E9-32EA-4B34-A18E-CE1D863241C8}";

            #endregion

        }

        public class BasePage
        {
            #region PageMetaData
            public const string MetaDescription = @"{1AD7B378-5683-4FE8-8428-BFBEBF7C1951}";
            public const string MetaTitle = @"{088E319C-4779-4409-9090-8E4DCC782460}";

            #endregion

        }

        public class Common
        {
            public class ImageLink
            {
                #region LinkSection
                public const string HideFromMobile = @"{83FB5DD6-B581-45BF-9DBE-00536FE2C5B8}";
                public const string Image = @"{ED891863-E1F5-4515-9A7E-8661E01446B7}";
                public const string Text = @"{AE91F1E7-D427-45A5-B9D8-F84CD8449890}";

                #endregion

            }
            public class SimpleLink
            {
                #region LinkSection
                public const string Link = @"{43AD1713-85B4-4D43-AE75-79AE74BBC972}";

                #endregion

            }
            public class SimpleText
            {
                #region TextSection
                public const string Text = @"{EDF20D66-DBBE-485B-88B2-B4EC99309E65}";

                #endregion

            }

        }

        public class Components
        {
            public class Carousel
            {
                public class CarouselItemBase
                {


                }
                public class CarouselItemHero
                {
                    #region Main
                    public const string HeaderText = @"{C2E15D39-B714-4377-8B35-38352B4BDE67}";
                    public const string Image = @"{192163FD-0E0A-41A4-9F98-330D9B78A99B}";

                    #endregion

                }
                public class CarouselMain
                {
                    #region Main
                    public const string HeaderText = @"{19B32F82-235E-4D88-A251-11FAF471A968}";

                    #endregion
                    #region Settings
                    public const string AutoPlay = @"{A7B647EA-09EA-4683-BB74-B12BC5BB31A7}";
                    public const string Delay = @"{BC3A2873-E0FB-44DF-AE4F-C2D0307E1247}";
                    public const string ShowArrows = @"{A4BBE1A2-AABD-4732-8A24-65B58C8185CC}";

                    #endregion

                }

            }
            public class HomeCarousel
            {
                public class HomeCarouselItemBase
                {


                }
                public class HomeCarouselItemHero
                {
                    #region Main
                    public const string Link = @"{BC713194-8155-48F5-8A7C-7FBD8F120A73}";

                    #endregion

                }
                public class HomeCarouselMain
                {


                }

            }
            public class MobileModule
            {
                public class MobileModuleItem
                {
                    #region Main
                    public const string IconClass = @"{25EA49DA-92D0-4D39-82C6-E706D54E17D3}";
                    public const string Link = @"{F2DE2BAA-BA7F-4683-AD5A-CED5A4971F10}";

                    #endregion

                }
                public class MobileModuleItemBase
                {


                }
                public class MobileModuleMain
                {


                }

            }
            public class Module
            {
                public class ModuleItem
                {
                    #region Main
                    public const string ButtonLink = @"{F4DCF117-0D20-47AA-A365-971B3C4A8981}";
                    public const string Image = @"{290B15FA-4208-4931-844C-E31ED3FD35E0}";
                    public const string SubTitle = @"{D253C2D0-43E7-4027-93B5-3E4DAB770046}";
                    public const string Title = @"{91622FD7-FFE0-4314-BBD8-9E2FFD0738F4}";

                    #endregion

                }
                public class ModuleItemBase
                {


                }
                public class ModuleMain
                {


                }

            }
            public class NewsFeed
            {
                public class NewsRoom
                {
                    #region Main
                    public const string Title = @"{6E0A8ABF-97A2-4195-B5F8-C047A997907D}";

                    #endregion

                }

            }
            public class Strip
            {
                public class ChooseaDealerLink
                {
                    #region LinkSection
                    public const string AfterselectingadealerLink = @"{2BE9F178-F990-4778-853E-A8AF6089F8EF}";
                    public const string AfterselectingadealerText = @"{940528CC-05FF-4C71-8367-EEDE1A60D43E}";
                    public const string InitialText = @"{28337710-9E01-42EE-9729-2558B6EBE288}";

                    #endregion

                }
                public class StripMain
                {


                }

            }

        }

        public class Footer
        {
            public class FooterCarHubsMain
            {


            }
            public class FooterContactNewsTvMain
            {


            }
            public class FooterCopyrightMain
            {


            }
            public class FooterMain
            {


            }
            public class FooterMobileMain
            {


            }
            public class FooterSocialMediaMain
            {


            }
            public class FooterToyotaRowOneMain
            {


            }
            public class FooterToyotaRowTwoMain
            {


            }

        }

        public class Header
        {
            public class HeaderMain
            {


            }
            public class HeaderMobileMain
            {


            }
            public class HeaderWebMain
            {


            }

        }

        public class ConfigurationFolder
        {


        }

        public class LinksFolder
        {


        }

        public class RenderingParameters
        {
            public class FooterAppearence
            {
                #region Appearence
                public const string HideForMobile = @"{84072880-EEC1-4A86-BF29-724FB31A582C}";

                #endregion

            }
            public class Page
            {
                #region Styles
                public const string BodyClass = @"{32D3F433-027C-48BC-922C-BF1BF79BC0E4}";

                #endregion

            }
            public class Scripts
            {
                #region Scripts
                public const string JsPath = @"{C2227831-0F83-4A02-BA54-38C650BCEFF6}";

                #endregion

            }
            public class Styles
            {
                #region Styles
                public const string CssPath = @"{B71E96C8-FEF6-4B55-BB07-D4CC0711317F}";

                #endregion

            }

        }

        public class HomePage
        {


        }

        public class SiteSettings
        {
            #region Analytics
            public const string GATrackingCode = @"{2D73246F-D55A-4292-808F-B9E99688BB50}";

            #endregion
            #region Footer
            public const string CopyrightText = @"{F8599707-3477-4657-B008-5ECBAE92FD6F}";

            #endregion
            #region Header
            public const string SiteLogo = @"{1F7BEC2C-388E-4E28-B989-84A5EF567E57}";
            public const string SiteName = @"{DAA2582D-30CA-4369-9ED2-8D6B0CEEFC23}";
            public const string SiteURL = @"{8532FD14-88BE-4DC6-BFDE-68A95870F130}";

            #endregion
            #region SocialMedia
            public const string SocialMediaImage = @"{1DC76C97-8982-4C61-A3C8-6164D8230664}";
            public const string TwitterSiteName = @"{089676CB-CC26-4B44-BFC0-4B306DC60DB3}";

            #endregion

        }

    }
}