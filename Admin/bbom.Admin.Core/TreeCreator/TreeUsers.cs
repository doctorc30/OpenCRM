using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.TreeCreator.Json.TreeJson;
using bbom.Data.IdentityModel;
using bbom.Data.IdentityModelPartials.Comparers;

namespace bbom.Admin.Core.TreeCreator
{
    /// <summary>
    /// Класс для работы с деревом пользователя
    /// </summary>
    public class TreeUsers : ITreeUsers
    {
        private ICollection<AspNetUser> _usersInLine;

        /// <summary>
        /// Возвращает дерево пользователя в формате json
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object GetTreeUsersJson(AspNetUser user)
        {
            var nodesJson = new List<TreeNode>
            {
                new TreeNode
                {
                    id = user.UserName,
                    label = user.GetFullName(),
                    color = "#FB7E81",
                    @group = "users"
                }
            };
            var edgesJson = new List<Edge>();
            GetTreeJsonSubUser(user, nodesJson, edgesJson, false);
            return new { nodes = nodesJson.ToArray(), edges = edgesJson.ToArray() };
        }

        /// <summary>
        /// Рекурсивный обход дерева пользователя
        /// </summary>
        /// <param name="parentUser"></param>
        /// <param name="nodesJson"></param>
        /// <param name="edgesJson"></param>
        /// <param name="selectLine"></param>
        private void GetTreeJsonSubUser(AspNetUser parentUser, List<TreeNode> nodesJson, List<Edge> edgesJson, bool selectLine)
        {
            // все дочерние пользователи
            var childrens = parentUser.AspNetUsers1.Where(user => user.Foot != null).ToList();
            childrens.Sort(new AspNetUserComparer());
            foreach (var children in childrens)
            {
                var userNode = JsonHelper.CreateNodeUser(children.UserName);
                userNode.label = children.GetFullName();

                //условие на проверку выделения определенной линии
                if (selectLine && _usersInLine.Contains(children))
                {
                    userNode.color = "red";
                }

                nodesJson.Add(userNode);
                switch (childrens.Count)
                {
                    case 1:
                        {
                            nodesJson.Add(JsonHelper.CreateNodeEmpty(parentUser.UserName));
                            if (children.Foot == 0)
                            {
                                edgesJson.Add(new Edge { @from = parentUser.UserName, to = children.UserName });
                                edgesJson.Add(new Edge { @from = parentUser.UserName, to = parentUser.UserName + GlobalConstants.EmptyNodePostfix });
                            }
                            else
                            {
                                edgesJson.Add(new Edge { @from = parentUser.UserName, to = parentUser.UserName + GlobalConstants.EmptyNodePostfix });
                                edgesJson.Add(new Edge { @from = parentUser.UserName, to = children.UserName });
                            }
                            break;
                        }
                    default:
                        {
                            edgesJson.Add(new Edge { @from = parentUser.UserName, to = children.UserName });
                            break;
                        }
                }
                GetTreeJsonSubUser(children, nodesJson, edgesJson, selectLine);
            }
        }

        /// <summary>
        /// Возвращает дерево пользователя с подсветкой определенной линии
        /// </summary>
        /// <param name="user"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public object GetTreeUsersWithLineJson(AspNetUser user, int line)
        {
            var nodesJson = new List<TreeNode>
            {
                new TreeNode
                {
                    id = user.UserName,
                    label = user.UserName,
                    @group = "users"
                }
            };
            _usersInLine = CoreFasade.UsersHelper.GetAllChildren(user, line);
            var edgesJson = new List<Edge>();
            GetTreeJsonSubUser(user, nodesJson, edgesJson, true);
            return new { nodes = nodesJson.ToArray(), edges = edgesJson.ToArray() };
        } 
    }
}