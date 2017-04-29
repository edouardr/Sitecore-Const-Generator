namespace Sitecore.Helix.ConstGenerator.Tests.Repositories
{
    using System.Linq;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;
    using Sitecore.Helix.ConstGenerator.Infrastructure.Repositories;

    [TestFixture]
    public class TemplatesRepositoryTests
    {
        private ISitecoreWebApiRequestResult<Result, Item> _validRequestResult;

        [SetUp]
        public void Init()
        {
            #region JSON Response

            const string jsonResult = @"{
            'statusCode': 200,
            'result': {
                'totalCount': 16,
                'resultCount': 16,
                'items': [
                {
                    'Category': 'System',
                    'Database': 'master',
                    'DisplayName': 'Branches',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': 'Branches',
                    'Path': '/sitecore/templates/System/Branches',
                    'Template': 'System/Templates/Template Folder',
                    'TemplateId': '{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}',
                    'TemplateName': 'Template Folder',
                    'Url': '~/link.aspx?_id=6FBC11476B0F49DA90CE38DE0B806B44&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branch',
                    'Database': 'master',
                    'DisplayName': 'Data',
                    'HasChildren': false,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{E7C088EC-8938-412F-AEFE-B328A86F11E2}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{35E75C72-4985-4E09-88C3-0EAC6CD1E64F}/{E7C088EC-8938-412F-AEFE-B328A86F11E2}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Data',
                    'Path': '/sitecore/templates/System/Branches/Branch/Data',
                    'Template': 'System/Templates/Template section',
                    'TemplateId': '{E269FBB5-3750-427A-9149-7AA950B49301}',
                    'TemplateName': 'Template section',
                    'Url': '~/link.aspx?_id=E7C088EC8938412FAEFEB328A86F11E2&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branches',
                    'Database': 'master',
                    'DisplayName': 'Branch',
                    'HasChildren': true,
                    'Icon': '/~/icon/software/32x32/branch.png.aspx',
                    'ID': '{35E75C72-4985-4E09-88C3-0EAC6CD1E64F}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{35E75C72-4985-4E09-88C3-0EAC6CD1E64F}',
                    'MediaUrl': '/~/icon/software/48x48/branch.png.aspx',
                    'Name': 'Branch',
                    'Path': '/sitecore/templates/System/Branches/Branch',
                    'Template': 'System/Templates/Template',
                    'TemplateId': '{AB86861A-6030-46C5-B394-E8F99E8B87DB}',
                    'TemplateName': 'Template',
                    'Url': '~/link.aspx?_id=35E75C7249854E0988C30EAC6CD1E64F&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'System',
                    'Database': 'master',
                    'DisplayName': 'Branches',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': 'Branches',
                    'Path': '/sitecore/templates/System/Branches',
                    'Template': 'System/Templates/Template Folder',
                    'TemplateId': '{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}',
                    'TemplateName': 'Template Folder',
                    'Url': '~/link.aspx?_id=6FBC11476B0F49DA90CE38DE0B806B44&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Command Template',
                    'Database': 'master',
                    'DisplayName': 'Data',
                    'HasChildren': true,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{7BB0C20D-385A-429F-9D1F-1356A2020EC9}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B2613CC1-A748-46A3-A0DB-3774574BD339}/{7BB0C20D-385A-429F-9D1F-1356A2020EC9}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Data',
                    'Path': '/sitecore/templates/System/Branches/Command Template/Data',
                    'Template': 'System/Templates/Template section',
                    'TemplateId': '{E269FBB5-3750-427A-9149-7AA950B49301}',
                    'TemplateName': 'Template section',
                    'Url': '~/link.aspx?_id=7BB0C20D385A429F9D1F1356A2020EC9&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branches',
                    'Database': 'master',
                    'DisplayName': 'Command Template',
                    'HasChildren': true,
                    'Icon': '/~/icon/software/32x32/element_run.png.aspx',
                    'ID': '{B2613CC1-A748-46A3-A0DB-3774574BD339}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B2613CC1-A748-46A3-A0DB-3774574BD339}',
                    'MediaUrl': '/~/icon/software/48x48/element_run.png.aspx',
                    'Name': 'Command Template',
                    'Path': '/sitecore/templates/System/Branches/Command Template',
                    'Template': 'System/Templates/Template',
                    'TemplateId': '{AB86861A-6030-46C5-B394-E8F99E8B87DB}',
                    'TemplateName': 'Template',
                    'Url': '~/link.aspx?_id=B2613CC1A74846A3A0DB3774574BD339&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Data',
                    'Database': 'master',
                    'DisplayName': 'Command',
                    'HasChildren': false,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{854CC8F6-94AD-4521-A4B6-44ED8F794C98}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B2613CC1-A748-46A3-A0DB-3774574BD339}/{7BB0C20D-385A-429F-9D1F-1356A2020EC9}/{854CC8F6-94AD-4521-A4B6-44ED8F794C98}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Command',
                    'Path': '/sitecore/templates/System/Branches/Command Template/Data/Command',
                    'Template': 'System/Templates/Template field',
                    'TemplateId': '{455A3E98-A627-4B40-8035-E683A0331AC7}',
                    'TemplateName': 'Template field',
                    'Url': '~/link.aspx?_id=854CC8F694AD4521A4B644ED8F794C98&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Command Template',
                    'Database': 'master',
                    'DisplayName': 'Data',
                    'HasChildren': true,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{7BB0C20D-385A-429F-9D1F-1356A2020EC9}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B2613CC1-A748-46A3-A0DB-3774574BD339}/{7BB0C20D-385A-429F-9D1F-1356A2020EC9}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Data',
                    'Path': '/sitecore/templates/System/Branches/Command Template/Data',
                    'Template': 'System/Templates/Template section',
                    'TemplateId': '{E269FBB5-3750-427A-9149-7AA950B49301}',
                    'TemplateName': 'Template section',
                    'Url': '~/link.aspx?_id=7BB0C20D385A429F9D1F1356A2020EC9&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'System',
                    'Database': 'master',
                    'DisplayName': 'Branches',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': 'Branches',
                    'Path': '/sitecore/templates/System/Branches',
                    'Template': 'System/Templates/Template Folder',
                    'TemplateId': '{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}',
                    'TemplateName': 'Template Folder',
                    'Url': '~/link.aspx?_id=6FBC11476B0F49DA90CE38DE0B806B44&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Insert Rule',
                    'Database': 'master',
                    'DisplayName': 'Data',
                    'HasChildren': true,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{A16FB230-6642-4FC5-B115-1E3A838B1E83}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B4D19D07-B3EB-4F7D-98EC-8BCB41CCC58E}/{A16FB230-6642-4FC5-B115-1E3A838B1E83}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Data',
                    'Path': '/sitecore/templates/System/Branches/Insert Rule/Data',
                    'Template': 'System/Templates/Template section',
                    'TemplateId': '{E269FBB5-3750-427A-9149-7AA950B49301}',
                    'TemplateName': 'Template section',
                    'Url': '~/link.aspx?_id=A16FB23066424FC5B1151E3A838B1E83&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branches',
                    'Database': 'master',
                    'DisplayName': 'Insert Rule',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/form_blue.png',
                    'ID': '{B4D19D07-B3EB-4F7D-98EC-8BCB41CCC58E}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B4D19D07-B3EB-4F7D-98EC-8BCB41CCC58E}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/form_blue.png',
                    'Name': 'Insert Rule',
                    'Path': '/sitecore/templates/System/Branches/Insert Rule',
                    'Template': 'System/Templates/Template',
                    'TemplateId': '{AB86861A-6030-46C5-B394-E8F99E8B87DB}',
                    'TemplateName': 'Template',
                    'Url': '~/link.aspx?_id=B4D19D07B3EB4F7D98EC8BCB41CCC58E&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Data',
                    'Database': 'master',
                    'DisplayName': 'Type',
                    'HasChildren': false,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{4A6DFA44-4706-4B98-8C88-ADA44F8FDB7C}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B4D19D07-B3EB-4F7D-98EC-8BCB41CCC58E}/{A16FB230-6642-4FC5-B115-1E3A838B1E83}/{4A6DFA44-4706-4B98-8C88-ADA44F8FDB7C}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Type',
                    'Path': '/sitecore/templates/System/Branches/Insert Rule/Data/Type',
                    'Template': 'System/Templates/Template field',
                    'TemplateId': '{455A3E98-A627-4B40-8035-E683A0331AC7}',
                    'TemplateName': 'Template field',
                    'Url': '~/link.aspx?_id=4A6DFA4447064B988C88ADA44F8FDB7C&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Insert Rule',
                    'Database': 'master',
                    'DisplayName': 'Data',
                    'HasChildren': true,
                    'Icon': '/~/icon/Applications/32x32/Document.png.aspx',
                    'ID': '{A16FB230-6642-4FC5-B115-1E3A838B1E83}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{B4D19D07-B3EB-4F7D-98EC-8BCB41CCC58E}/{A16FB230-6642-4FC5-B115-1E3A838B1E83}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/document.png',
                    'Name': 'Data',
                    'Path': '/sitecore/templates/System/Branches/Insert Rule/Data',
                    'Template': 'System/Templates/Template section',
                    'TemplateId': '{E269FBB5-3750-427A-9149-7AA950B49301}',
                    'TemplateName': 'Template section',
                    'Url': '~/link.aspx?_id=A16FB23066424FC5B1151E3A838B1E83&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'System',
                    'Database': 'master',
                    'DisplayName': 'Branches',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': 'Branches',
                    'Path': '/sitecore/templates/System/Branches',
                    'Template': 'System/Templates/Template Folder',
                    'TemplateId': '{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}',
                    'TemplateName': 'Template Folder',
                    'Url': '~/link.aspx?_id=6FBC11476B0F49DA90CE38DE0B806B44&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branch Folder',
                    'Database': 'master',
                    'DisplayName': '__Standard Values',
                    'HasChildren': false,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{B3FDAF04-F1D6-45C6-BEB7-958C54B224F4}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{85ADBF5B-E836-4932-A333-FE0F9FA1ED1E}/{B3FDAF04-F1D6-45C6-BEB7-958C54B224F4}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': '__Standard Values',
                    'Path': '/sitecore/templates/System/Branches/Branch Folder/__Standard Values',
                    'Template': 'System/Branches/Branch Folder',
                    'TemplateId': '{85ADBF5B-E836-4932-A333-FE0F9FA1ED1E}',
                    'TemplateName': 'Branch Folder',
                    'Url': '~/link.aspx?_id=B3FDAF04F1D645C6BEB7958C54B224F4&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                },
                {
                    'Category': 'Branches',
                    'Database': 'master',
                    'DisplayName': 'Branch Folder',
                    'HasChildren': true,
                    'Icon': '/temp/iconcache/applications/16x16/folder.png',
                    'ID': '{85ADBF5B-E836-4932-A333-FE0F9FA1ED1E}',
                    'Language': 'en',
                    'LongID': '/{11111111-1111-1111-1111-111111111111}/{3C1715FE-6A13-4FCF-845F-DE308BA9741D}/{4BF98EF5-1D09-4DD1-9AFE-795F9829FD44}/{6FBC1147-6B0F-49DA-90CE-38DE0B806B44}/{85ADBF5B-E836-4932-A333-FE0F9FA1ED1E}',
                    'MediaUrl': '/temp/iconcache/applications/48x48/folder.png',
                    'Name': 'Branch Folder',
                    'Path': '/sitecore/templates/System/Branches/Branch Folder',
                    'Template': 'System/Templates/Template',
                    'TemplateId': '{AB86861A-6030-46C5-B394-E8F99E8B87DB}',
                    'TemplateName': 'Template',
                    'Url': '~/link.aspx?_id=85ADBF5BE8364932A333FE0F9FA1ED1E&amp;_z=z',
                    'Version': 1,
                    'Fields': {}
                }
                ]
            }
        }";

            #endregion

            _validRequestResult = JsonConvert.DeserializeObject<RequestResult>(jsonResult);
        }
        
        [Test]
        public void CanCreateTreeNode()
        {
            const string sitecoreRootPath = @"sitecore/templates/system/branches";
            const int rootItemsCount = 1;
            const int childrenItemsCountForFirstNode = 4;
            const string firstChildName = @"Branch";

            var webApiRepository = new Mock<ISitecoreWebApiRepository>();
            webApiRepository
                .Setup(r => r.RequestItemsAsync(It.IsAny<SitecoreActionType>(), It.IsAny<string>()))
                .ReturnsAsync(_validRequestResult);

            var repository = new TemplatesRepository(webApiRepository.Object, sitecoreRootPath);

            var tree = repository.CreateTree().ToList();

            Assert.NotNull(tree);
            Assert.AreEqual(rootItemsCount, tree.Count);
            Assert.AreEqual(childrenItemsCountForFirstNode, tree.First().Children.Count);
            Assert.AreEqual(firstChildName, tree.First().Children.First().Value.Name);
        }
    }
}