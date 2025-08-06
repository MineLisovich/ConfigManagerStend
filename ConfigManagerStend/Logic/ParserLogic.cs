using ConfigManagerStend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ConfigManagerStend.Logic
{
    internal class ParserLogic
    {
        public bool ParserFile(ParserModel parser)
        {
       
            try
            {
                // Читаем содержимое исходного файла
                string jsonContent = File.ReadAllText(parser.JsonFilePath);

                // Парсим JSON
                var jsonNode = JsonNode.Parse(jsonContent);

                // Изменяем параметр Path (предполагая, что это свойство верхнего уровня)
                if (jsonNode != null && jsonNode["Path"] != null)
                {
                    jsonNode["Path"] = parser.DebugPath; // Замените на нужное значение
                }

                string modifiedJson = jsonNode?.ToJsonString();

                // Записываем измененный JSON в новый файл
                File.WriteAllText(Path.Combine(parser.JsonPathSave, "_" + parser.JsonFileName), modifiedJson);
            }
            catch (Exception ex)
            {
               return false;
            }
            return true;
        }
    }
}
