using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("Time scales - TAI")]
internal class When_converting_TAI_to_and_from_TAI
{
    private static TAI               Tai;
    private static ITimeScaleContext Ctx;
    private static double            Input;
    private static double            ToTai;
    private static double            FromTai;

    private Establish context = () =>
    {
        Tai = new TAI();
        Ctx = new BasicTimeScaleContext();
        Input = 12345.6789;
    };

    private Because of = () =>
    {
        ToTai = Tai.ToTaiSeconds(Input, Ctx);
        FromTai = Tai.FromTaiSeconds(Input, Ctx);
    };

    private It should_leave_value_unchanged_when_converting_from_tai = () => FromTai.ShouldEqual(Input);

    private It should_leave_value_unchanged_when_converting_to_tai = () => ToTai.ShouldEqual(Input);
}