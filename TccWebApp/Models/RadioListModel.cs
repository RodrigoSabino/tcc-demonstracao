using System.Collections.Generic;

namespace TccWebApp.Models
{
    public static class RadioListModel
    {
        public static IEnumerable<Radio> RadioList = new List<Radio>
        {
            new Radio { id = "http://evpp.mm.uol.com.br/band/bandnewsfm_rj/playlist.m3u8", name = "BandNews" },
            new Radio { id = "rtmp://media.sgr.globo.com:1935/CBNRD/cbnsp.sdp", name = "CBN" },
            new Radio { id = "http://cast.hoost.com.br:8239/stream", name = "Potência Total" },
            new Radio { id = "http://server01.ouvir.radio.br:8006/stream", name = "Jazz Medley" }
        };
    }
}