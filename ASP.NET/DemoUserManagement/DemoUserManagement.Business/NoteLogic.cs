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
        public static DataTable LoadNotes(string objectID, int objectType)
        {
            return NotesDataAccess.GetNote(objectID, objectType);
        }

        public static void AddNote(string note,string objectID, int objectType)
        {
            NotesDataAccess.AddNote(note, objectID, objectType);
        }
    }
}
