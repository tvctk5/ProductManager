using System;

namespace VSW.Lib.Global
{
    public class Permissions
    {
        public Permissions()
        {

        }

        public Permissions(int access)
        {
            Access = access;
        }

        internal void ORAccess(int access)
        {
            Permissions newPermissions = new Permissions(access);

            this.View = this.View || newPermissions.View;
            this.Add = this.Add || newPermissions.Add;
            this.Edit = this.Edit || newPermissions.Edit;
            this.Delete = this.Delete || newPermissions.Delete;
            this.Approve = this.Approve || newPermissions.Approve;
        }

        private int _Access = 0;
        public int Access
        {
            get { return _Access; }
            internal set { _Access = value; }
        }

        public bool Control
        {
            get { return (Access & 16) == 16; }
            internal set
            {
                if (Approve && !value) Access = Access - 16;
                if (!Approve && value) Access = Access + 16;
            }
        }
        public bool Approve
        {
            get { return (Access & 16) == 16; }
            internal set
            {
                if (Approve && !value) Access = Access - 16;
                if (!Approve && value) Access = Access + 16;
            }
        }
        public bool Delete
        {
            get { return (Access & 8) == 8; }
            internal set
            {
                if (Delete && !value) Access = Access - 8;
                if (!Delete && value) Access = Access + 8;
            }
        }
        public bool Edit
        {
            get { return (Access & 4) == 4; }
            internal set
            {
                if (Edit && !value) Access = Access - 4;
                if (!Edit && value) Access = Access + 4;
            }
        }
        public bool Add
        {
            get { return (Access & 2) == 2; }
            internal set
            {
                if (Add && !value) Access = Access - 2;
                if (!Add && value) Access = Access + 2;
            }
        }
        public bool View
        {
            get { return (Access & 1) == 1; }
            internal set
            {
                if (View && !value) Access = Access - 1;
                if (!View && value) Access = Access + 1;
            }
        }
        public bool Full
        {
            get{
                return View && Add && Edit && Delete && Approve;
            }
            internal set
            {
                View = Add = Edit = Delete = Approve = value;
            }
        }
        public bool Any
        {
            get
            {
                return Access > 0;
            }
        }
    }
}
