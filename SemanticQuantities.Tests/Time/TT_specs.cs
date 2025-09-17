using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("Time scales - TT")]
internal class When_converting_TT_to_and_from_TAI
{
    private static TT                Tt;
    private static ITimeScaleContext Ctx;
    private static double            Input;
    private static double            ToTai;
    private static double            FromTai;

    private Establish context = () =>
    {
        Tt = new TT();
        Ctx = new BasicTimeScaleContext();
        Input = 1000.0; // seconds since epoch in TT
    };

    private Because of = () =>
    {
        ToTai = Tt.ToTaiSeconds(Input, Ctx);
        FromTai = Tt.FromTaiSeconds(ToTai, Ctx);
    };

    private It should_round_trip_back_to_original = () => FromTai.ShouldEqual(Input);

    private It should_subtract_32_184_seconds_when_converting_to_tai = () => ToTai.ShouldEqual(Input - 32.184);
}