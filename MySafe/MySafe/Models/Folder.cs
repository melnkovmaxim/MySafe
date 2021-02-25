using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public string Name { get; set; }
        public string Header => NameBlocks.Length > 1 ? NameBlocks[0] : string.Empty;
        public string Body => NameBlocks.Length > 1 ? NameBlocks[1] : NameBlocks[0];
        private string[] NameBlocks => Name.Split(":");
    }
}
