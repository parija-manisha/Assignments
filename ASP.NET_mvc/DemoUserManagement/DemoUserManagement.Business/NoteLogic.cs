using DemoUserManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Business
{
    public class NoteLogic
    {
        public static List<Note> GetNotes(int userId, int pageName)
        {
            return NotesDataAccess.GetNotes(userId, pageName);
        }


        public static void AddNote(string note,int objectID, int objectType)
        {
            NotesDataAccess.AddNote(note, objectID, objectType);
        }

        public static int CountNote(int userId, int objectType)
        {
            return NotesDataAccess.CountNote(userId, objectType);
        }
    }
}
