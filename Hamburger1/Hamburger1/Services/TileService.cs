using Hamburger1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hamburger1.Services
{
    public class TileService
    {
        public static Windows.Data.Xml.Dom.XmlDocument CreateTiles(TodoItem work)
        {
            XDocument xDoc = new XDocument(
                                new XElement("tile", new XAttribute("version", 3),
                                    new XElement("visual",

                                        // Small Tile
                                        new XElement("binding", new XAttribute("branding", "name"), new XAttribute("displayName", "ToDoList"), new XAttribute("template", "TileSmall"),
                                            new XElement("group",
                                                new XElement("subgroup",
                                                    new XElement("text", work.Title, new XAttribute("hint-style", "caption")),
                                                    new XElement("text", work.Description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 2)),
                                                    new XElement("text", work.DueDate.ToString(), new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 1))
                                                            //new XElement("image", new XAttribute("src", "Assets/水墨框"),new XAttribute("placement", "background"))
                                                            )
                                                         )
                                                     ),

                                        // Medium Tile
                                        new XElement("binding", new XAttribute("branding", "name"), new XAttribute("displayName", "ToDoList"), new XAttribute("template", "TileMedium"),
                                            new XElement("group",
                                                new XElement("subgroup",
                                                    new XElement("text", work.Title, new XAttribute("hint-style", "caption")),
                                                    new XElement("text", work.Description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 2)),
                                                    new XElement("text", work.DueDate.ToString(), new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 1))
                                                            )
                                                         )
                                                     ),

                                        // Wide Tile
                                        new XElement("binding", new XAttribute("branding", "name"), new XAttribute("displayName", "ToDoList"), new XAttribute("template", "TileWide"),
                                            new XElement("group",
                                                new XElement("subgroup",
                                                    //new XElement("image", new XAttribute("src", "Assets/落叶.jpg"), new XAttribute("placement", "background"), new XAttribute("hint-align", "stretch")),
                                                    new XElement("text", work.Title, new XAttribute("hint-style", "caption")),
                                                    new XElement("text", work.Description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 2)),
                                                    new XElement("text", work.DueDate.ToString(), new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 1))
                                                            )
                                                         )
                                                     )
                                                  )
                                              )
                                           );

            Windows.Data.Xml.Dom.XmlDocument xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }
    }
}
