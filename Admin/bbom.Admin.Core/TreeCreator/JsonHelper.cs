using bbom.Admin.Core.TreeCreator.Json.TreeJson;

namespace bbom.Admin.Core.TreeCreator
{
    internal static class JsonHelper
    {
        public static TreeNode CreateNodeUser(string userName)
        {
            return new TreeNode
            {
                id = userName,
                label = userName,
                @group = "users"
            };
        }

        public static TreeNode CreateNodeEmpty(string userName)
        {
            return new TreeNode
            {
                id = userName + "empt",
                color = new {background = "white", border = "grey"},
                @group = "users"
            };
        }
    }
}