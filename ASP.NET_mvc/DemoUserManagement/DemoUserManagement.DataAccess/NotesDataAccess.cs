using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUserManagement.Util;
using static DemoUserManagement.Util.Constants;

namespace DemoUserManagement.DataAccess
{
    public class NotesDataAccess
    {
        public static void AddNote(string note, int objectId, int objectType)
        {
            try
            {
                if (objectId != -1)
                {
                    using (var context = new UserManagementTableEntities())
                    {
                        var newNote = new Note
                        {
                            ObjectID = objectId,
                            ObjectType = objectType,
                            NoteText = note,
                            TimeStamp = DateTime.Now,
                        };

                        context.Notes.Add(newNote);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while inserting into DocumentList", ex);
            }
        }

        public static List<Note> GetNotes(int userId, int pageName)
        {
            try
            {
                using (var context = new UserManagementTableEntities())
                {
                    var notes = context.Notes
                        .Where(n => n.ObjectID == userId && n.ObjectType == pageName)
                        .OrderByDescending(n => n.TimeStamp)
                        .ToList();

                    return notes;
                }
            }
            catch (Exception ex)
            {
                Logger.AddError("Error while retrieving notes", ex);
                return new List<Note>();
            }
        }


        public static int CountNote(int userId, int objectType)
        {
            using (var context = new UserManagementTableEntities())
            {
                int count = context.Notes.Where(n => n.ObjectID == userId && n.ObjectType == objectType).Count();

                return count;
            }
        }
    }
}
