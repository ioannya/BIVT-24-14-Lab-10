using System;
using System.IO;
using System.Text;
using System.Windows.Forms; // Добавляем using для MessageBox

namespace Model
{
    public class ScoreManager : IScoreSavable
    {
        private const string FileName = "scores.json";

        public ScoreEntry[] Load()
        {
            try
            {
                if (!File.Exists(FileName))
                    return new ScoreEntry[0];

                string[] lines = File.ReadAllLines(FileName);
                ScoreEntry[] result = new ScoreEntry[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('|');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        result[i] = new ScoreEntry
                        {
                            Player = parts[0],
                            Score = score
                        };
                    }
                    else
                    {
                        // Обработка ошибки в строке
                        MessageBox.Show($"Ошибка в файле рекордов, строка: {lines[i]}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return new ScoreEntry[0]; // Возвращаем пустой массив
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке рекордов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new ScoreEntry[0];
            }
        }

        public void Save(ScoreEntry[] entries)
        {
            try
            {
                Array.Sort(entries, (a, b) => b.Score.CompareTo(a.Score));

                int count = entries.Length < 10 ? entries.Length : 10;

                using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.UTF8))
                {
                    for (int i = 0; i < count; i++)
                    {
                        sw.WriteLine(entries[i].Player + "|" + entries[i].Score);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении рекордов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
