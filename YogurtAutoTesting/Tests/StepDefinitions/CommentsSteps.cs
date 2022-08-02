using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class CommentsSteps
    {
        private CommentsClient _commentsClient;
        public CommentsSteps()
        {
            _commentsClient = new CommentsClient();
        }
    }
}
