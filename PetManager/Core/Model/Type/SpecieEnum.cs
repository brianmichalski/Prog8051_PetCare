using System.ComponentModel;
using System.Text;
using PetManager.Core.Model.Type.Tools;

namespace PetManager.Core.Model.Type;

public enum SpecieEnum
{
    [Description("Cat")]
    Cat,
    [Description("Dog")]
    Dog,
    [Description("Guinea Pig")]
    GuineaPig,
    [Description("Turtle")]
    Turtle
}