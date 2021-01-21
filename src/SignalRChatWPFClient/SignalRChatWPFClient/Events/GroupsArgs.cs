using System.Collections.Generic;

namespace SignalRChatWPFClient.Events
{
    public delegate void GroupsEventHandler(object sender, GroupsArgs e);

    public class GroupsArgs
    {
        public List<string> Groups { get; private set; }

        public GroupsArgs(List<string> Groups)
        {
            this.Groups = Groups;
        }
    }
}