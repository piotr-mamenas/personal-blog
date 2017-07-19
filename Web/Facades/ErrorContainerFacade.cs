namespace PersonalBlog.Web.Facades
{
    public class ErrorContainerFacade
    {
        private ErrorContainerFacade()
        {
            
        }

        public static ErrorContainerFacade Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorContainerFacade();
                }
                return _instance;
            }
        }

        private static ErrorContainerFacade _instance;
    }
}