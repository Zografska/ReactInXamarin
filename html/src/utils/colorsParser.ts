export const ParseToHexColor = (colorId: number) : string =>
{
    switch (colorId)
    {
        case 0:
            //Platinum(light gray)
            return "#f2f2f2";
        case 1:
            //Nickel
            return "#cccccc";
        case 2:
            //Cadmium Red
            return "#ff3232";
        case 3:
            //Goldenrod
            return "#ffa200";
        case 4:
            //Jade
            return "#30ff00";
        case 5:
            //DeepSkyBlue
            return "#5ff2ff";
        case 6:
            //Eminence
            return "#f47bff";
        case 7:
            //Jet
            return "#999999";
        case 8:
            //Aureolin
            return "#ffe600";
    }

    return "#f2f2f2";
}