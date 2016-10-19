using Microsoft.Data.OData;
using Microsoft.Data.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Query.Validators;

namespace OwinSelfhostSample
{
    public class MyOrderByValidator : OrderByQueryValidator
    {
        // Disallow the 'desc' parameter for $orderby option.
        public override void Validate(OrderByQueryOption orderByOption,
                                        ODataValidationSettings validationSettings)
        {
            if (orderByOption.OrderByNodes.Any(
                    node => node.Direction == OrderByDirection.Descending))
            {
                throw new ODataException("The 'desc' option is not supported.");
            }
            base.Validate(orderByOption, validationSettings);
        }
    }
}
