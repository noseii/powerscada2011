
namespace mymodel
{
    public class RolHakki : Entity
    {
        public mymodel.myenum.GorevTuru Rol {get;set; }
       
        private mymodel.myenum.Hak hak;

        public mymodel.myenum.Hak Hak
        {
            get
            {
                return hak;
            }

            set
            {
                hak = value;
            }

        }

        public bool Var { get; set; }

        public bool Engelle { get; set; }
       
    }
}
