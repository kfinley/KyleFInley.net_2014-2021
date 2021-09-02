function Analytics(options) {

    this.allTimeClickInfo = options.data.allTimeClickInfo;
    this.monthClickInfo = options.data.monthClickInfo;
    this.weekClickInfo = options.data.weekClickInfo;
    this.dayClickInfo = options.data.dayClickInfo;
    this.twoHoursClickInfo = options.data.twoHoursClickInfo;

    this.chartDivs = options.chartDivs;

    this.currentView = null;

    switch (options.currentView) {
        case Analytics.views.AllTime:
            this.currentView = this.allTimeClickInfo;
            break;
        case Analytics.views.Month:
            this.currentView = this.monthClickInfo;
            break;
        case Analytics.views.Week:
            this.currentView = this.weekClickInfo;
            break;
        case Analytics.views.Day:
            this.currentView = this.dayClickInfo;
            break;
        case Analytics.views.TwoHours:
            this.currentView = this.twoHoursClickInfo;
            break;
        default:
            throw new Error("No valid view was provided.");
    }
    /*var countriesMap = { "east timor": "TP", "samoa": "WS", "japan": "JP", "french southern territories": "TF", "tokelau": "TK", "cayman islands": "KY", "azerbaijan": "AZ", "north korea": "KP", "djibouti": "DJ", "french guiana": "GF", "malta": "MT", "guinea-bissau": "GW", "hungary": "HU", "taiwan": "TW", "cyprus": "CY", "haiti": "HT", "barbados": "BB", "eastern asia": "UN030", "bhutan": "BT", "yugoslavia": "YU", "lithuania": "LT", "congo - kinshasa": "CD", "micronesia": "UN057", "andorra": "AD", "union of soviet socialist republics": "SU", "rwanda": "RW", "aruba": "AW", "liberia": "LR", "argentina": "AR", "norway": "NO", "sierra leone": "SL", "somalia": "SO", "ghana": "GH", "falkland islands": "FK", "belarus": "BY", "saint helena": "SH", "cuba": "CU", "middle africa": "UN017", "central asia": "UN143", "french polynesia": "PF", "southern europe": "UN039", "guatemala": "GT", "isle of man": "IM", "belgium": "BE", "world": "UN001", "congo - brazzaville": "CG", "southern asia": "UN034", "kazakhstan": "KZ", "burkina faso": "BF", "aland islands": "AX", "kyrgyzstan": "KG", "netherlands": "NL", "portugal": "PT", "central america": "UN013", "denmark": "DK", "philippines": "PH", "montserrat": "MS", "senegal": "SN", "moldova": "MD", "latvia": "LV", "croatia": "HR", "bosnia and herzegovina": "BA", "chad": "TD", "switzerland": "CH", "western europe": "UN155", "mali": "ML", "bulgaria": "BG", "jamaica": "JM", "albania": "AL", "angola": "AO", "colombia": "CO", "serbia and montenegro": "CS", "northern america": "UN021", "palestinian territory": "PS", "lebanon": "LB", "malaysia": "MY", "christmas island": "CX", "mozambique": "MZ", "greece": "GR", "zaire": "ZR", "nicaragua": "NI", "new zealand": "NZ", "southern africa": "UN018", "canada": "CA", "afghanistan": "AF", "qatar": "QA", "oceania": "UN009", "palau": "PW", "turkmenistan": "TM", "equatorial guinea": "GQ", "pitcairn": "PN", "guinea": "GN", "panama": "PA", "nepal": "NP", "central african republic": "CF", "luxembourg": "LU", "solomon islands": "SB", "south america": "UN005", "swaziland": "SZ", "cook islands": "CK", "tuvalu": "TV", "netherlands antilles": "AN", "namibia": "NA", "nauru": "NR", "venezuela": "VE", "australia and new zealand": "UN053", "outlying oceania": "QO", "europe": "UN150", "brunei": "BN", "iran": "IR", "british indian ocean territory": "IO", "united arab emirates": "AE", "south georgia and the south sandwich islands": "GS", "saint kitts and nevis": "KN", "sri lanka": "LK", "paraguay": "PY", "china": "CN", "armenia": "AM", "western asia": "UN145", "kiribati": "KI", "belize": "BZ", "tunisia": "TN", "ukraine": "UA", "melanesia": "UN054", "yemen": "YE", "northern mariana islands": "MP", "libya": "LY", "trinidad and tobago": "TT", "mayotte": "YT", "gambia": "GM", "finland": "FI", "macedonia": "MK", "americas": "UN019", "mauritius": "MU", "antigua and barbuda": "AG", "niue": "NU", "syria": "SY", "dominican republic": "DO", "people's democratic republic of yemen": "YD", "jersey": "JE", "burma": "BU", "pakistan": "PK", "romania": "RO", "seychelles": "SC", "metropolitan france": "FX", "czech republic": "CZ", "myanmar": "MM", "el salvador": "SV", "egypt": "EG", "neutral zone": "NT", "guam": "GU", "africa": "UN002", "papua new guinea": "PG", "wallis and futuna": "WF", "united states": "US", "austria": "AT", "greenland": "GL", "mongolia": "MN", "ivory coast": "CI", "thailand": "TH", "honduras": "HN", "niger": "NE", "fiji": "FJ", "comoros": "KM", "turkey": "TR", "united kingdom": "GB", "madagascar": "MG", "iraq": "IQ", "bangladesh": "BD", "mauritania": "MR", "eastern europe": "UN151", "bolivia": "BO", "uruguay": "UY", "france": "FR", "bahamas": "BS", "vatican": "VA", "slovakia": "SK", "gibraltar": "GI", "ireland": "IE", "laos": "LA", "british virgin islands": "VG", "south korea": "KR", "anguilla": "AI", "malawi": "MW", "ecuador": "EC", "israel": "IL", "peru": "PE", "algeria": "DZ", "serbia": "RS", "tanzania": "TZ", "puerto rico": "PR", "montenegro": "ME", "tajikistan": "TJ", "svalbard and jan mayen": "SJ", "togo": "TG", "jordan": "JO", "chile": "CL", "martinique": "MQ", "oman": "OM", "turks and caicos islands": "TC", "nigeria": "NG", "spain": "ES", "sao tome and principe": "ST", "georgia": "GE", "eastern africa": "UN014", "bouvet island": "BV", "asia": "UN142", "northern europe": "UN154", "american samoa": "AS", "polynesia": "UN061", "morocco": "MA", "sweden": "SE", "heard island and mcdonald islands": "HM", "gabon": "GA", "guyana": "GY", "western africa": "UN011", "grenada": "GD", "guadeloupe": "GP", "hong kong": "HK", "russia": "RU", "u.s. virgin islands": "VI", "cocos islands": "CC", "bahrain": "BH", "zimbabwe": "ZW", "estonia": "EE", "mexico": "MX", "reunion": "RE", "india": "IN", "new caledonia": "NC", "lesotho": "LS", "antarctica": "AQ", "australia": "AU", "saint vincent and the grenadines": "VC", "saint pierre and miquelon": "PM", "uganda": "UG", "burundi": "BI", "kenya": "KE", "macao": "MO", "botswana": "BW", "italy": "IT", "western sahara": "EH", "south africa": "ZA", "east germany": "DD", "cambodia": "KH", "ethiopia": "ET", "bermuda": "BM", "vanuatu": "VU", "marshall islands": "MH", "cameroon": "CM", "zambia": "ZM", "benin": "BJ", "brazil": "BR", "saudi arabia": "SA", "singapore": "SG", "faroe islands": "FO", "iceland": "IS", "saint lucia": "LC", "monaco": "MC", "costa rica": "CR", "united states minor outlying islands": "UM", "slovenia": "SI", "germany": "DE", "caribbean": "UN029", "san marino": "SM", "dominica": "DM", "suriname": "SR", "eritrea": "ER", "tonga": "TO", "maldives": "MV", "south-eastern asia": "UN035", "uzbekistan": "UZ", "northern africa": "UN015", "norfolk island": "NF", "poland": "PL", "indonesia": "ID", "cape verde": "CV", "sudan": "SD", "liechtenstein": "LI", "vietnam": "VN", "guernsey": "GG", "kuwait": "KW" };*/
};

Analytics.views = { AllTime: "AllTime", Month: "Month", Week: "Week", Day: "Day", TwoHours: "TwoHours" };

Analytics.prototype.pageInit = function () {
    this.setChartTitleLines();
    $("#twoHoursView").click({ analytics: this }, function (e) {
        e.preventDefault();
        $("#clickCount").text(e.data.analytics.twoHoursClickInfo.totalClicks);
        e.data.analytics.currentView = e.data.analytics.twoHoursClickInfo;
        e.data.analytics.drawCharts();
        $(".actionItem").removeClass("selected");
        $(this).addClass("selected");

    })

    $("#dayView").click({ analytics: this }, function (e) {
        e.preventDefault();
        $("#clickCount").text(e.data.analytics.dayClickInfo.totalClicks);
        e.data.analytics.currentView = e.data.analytics.dayClickInfo;
        e.data.analytics.drawCharts();
        $(".actionItem").removeClass("selected");
        $(this).addClass("selected");
    })

    $("#weekView").click({ analytics: this }, function (e) {
        e.preventDefault();
        $("#clickCount").text(e.data.analytics.weekClickInfo.totalClicks);
        e.data.analytics.currentView = e.data.analytics.weekClickInfo;
        e.data.analytics.drawCharts();
        $(".actionItem").removeClass("selected");
        $(this).addClass("selected");
    })

    $("#monthView").click({ analytics: this }, function (e) {
        e.preventDefault();
        $("#clickCount").text(e.data.analytics.monthClickInfo.totalClicks);
        e.data.analytics.currentView = e.data.analytics.monthClickInfo;
        e.data.analytics.drawCharts();
        $(".actionItem").removeClass("selected");
        $(this).addClass("selected");
    })

    $("#allTimeView").click({ analytics: this }, function (e) {
        e.preventDefault();
        $("#clickCount").text(e.data.analytics.monthClickInfo.totalClicks);
        e.data.analytics.currentView = e.data.analytics.allTimeClickInfo;
        e.data.analytics.drawCharts();
        $(".actionItem").removeClass("selected");
        $(this).addClass("selected");
    })
}

Analytics.prototype.drawCharts = function () {
    analytics.drawClickTimeline(this.chartDivs.clickTimelineDiv);
    analytics.drawReferrersChart(this.chartDivs.referrersDiv);
    analytics.drawRegionsMap(this.chartDivs.regionsDiv);
    analytics.drawPlatformsChart(this.chartDivs.platformsDiv);
    analytics.drawBrowsersChart(this.chartDivs.browsersDiv);
};

Analytics.prototype.drawClickTimeline = function (chartDiv) {
    var data = new google.visualization.DataTable();
    data.addColumn('datetime', 'Date');
    data.addColumn('number', 'Clicks');

    var dates = new Array();

    var endDate = new Date(this.currentView.endTime * 1000);

    for (var i = 0; i < this.currentView.data.length; i++) {
        if (dates.length == 0) {
            dates.unshift(endDate);
        } else {
            endDate = new Date((Math.round(endDate.getTime() / 1000) - this.currentView.timeSize) * 1000);
            dates.unshift(endDate);
        }
    }
    for (var i = 0; i < this.currentView.data.length; i++) {
        data.addRow([dates[i], this.currentView.data[i]]);
    }

    var dateFormat;

    switch (this.currentView.view) {
        case Analytics.views.AllTime:
            dateFormat = "MMM yyyy";
            break;
        case Analytics.views.Month:
        case Analytics.views.Week:
            dateFormat = "MMM dd, yyyy";
            break;
        case Analytics.views.Day:
            dateFormat = "h:mm a";
            break;
        default:
    }
    var chart = new google.visualization.AreaChart(document.getElementById(chartDiv));
    var options = {
        height: 250,
        legend: {
            position: "none"
        },
        hAxis: {
            format: dateFormat,
            gridlines: {
                color: "#ebebeb",
                count: 8
            },
            textStyle: {
                color: "#999999",
                fontsize: 10
            },
            baselineColor: "#ebebeb"
        },
        vAxis: {
            gridlines: {
                color: "#ebebeb"
            },
            textPosition: "in",
            textStyle: {
                color: "#999999",
                fontsize: 10
            },
            baselineColor: "#ebebeb",
            viewWindow: {
                min: 0,
                max: this.currentView.totalClicks < 20 ? 20 : this.currentView.totalClicks + 20
            }
        },
        chartArea: { width: "100%", height: "75%", top: 20 },

    };

    chart.draw(data, options);

};

Analytics.prototype.drawReferrersChart = function (chartDiv) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Referrer');
    data.addColumn('number', 'Clicks');

    for (var i = 0; i < this.currentView.referrers.length; i++) {
        data.addRow([this.currentView.referrers[i].Id, this.currentView.referrers[i].Count]);
    }

    var chart = new google.visualization.PieChart(document.getElementById(chartDiv));
    var options = {
        pieHole: 0.8,
        legend: {
            position: "labeled"
        },
        height: 350,
        chartArea: { left: 20, top: 20, width: '100%', height: '80%' },
        pieSliceText: "none",
        colors: ["#427bd6", "#A2D200", "#FE9900", "#f0d202", "#994499", "#dd4477"]
    };

    chart.draw(data, options);
};

Analytics.prototype.drawRegionsMap = function (chartDiv) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Country');
    data.addColumn('number', 'Clicks');

    //for (var i = 0; i < countries.length; i++) {
    //    data.addRow([getKey(countriesMap, countries[i].Id).toTitleCase(), countries[i].Count]);
    //}

    for (var i = 0; i < this.currentView.regions.length; i++) {
        data.addRow([this.currentView.regions[i].Id, this.currentView.regions[i].Count]);
    }

    var options = { height: 350, colors: ["#a4bbcd", "#427bd6"] };

    var chart = new google.visualization.GeoChart(document.getElementById(chartDiv));
    chart.draw(data, options);
};

Analytics.prototype.drawBrowsersChart = function (chartDiv) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Browser');
    data.addColumn('number', 'Clicks');

    for (var i = 0; i < this.currentView.browsers.length; i++) {
        data.addRow([this.currentView.browsers[i].Id, this.currentView.browsers[i].Count]);
    }

    var chart = new google.visualization.BarChart(document.getElementById(chartDiv));
    var options = {
        legend: { position: "none" },
        colors: ['#a4bbcd'], height: 350, chartArea: { left: 100, top: 0, width: '75%', height: '80%' }, vaxis: { minValue: 0, maxValue: 20 },
        hAxis: {
            baselineColor: "#ebebeb"
        }
    };
    chart.draw(data, options);
};

Analytics.prototype.drawPlatformsChart = function (chartDiv) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Platform');
    data.addColumn('number', 'Clicks');

    for (var i = 0; i < this.currentView.platforms.length; i++) {
        data.addRow([this.currentView.platforms[i].Id, this.currentView.platforms[i].Count]);
    }

    var chart = new google.visualization.BarChart(document.getElementById(chartDiv));
    var options = {
        legend: { position: "none" },
        colors: ['#a4bbcd'], height: 350, chartArea: { left: 100, top: 0, width: '75%', height: '80%' }, vaxis: { minValue: 0, maxValue: 20 },
        hAxis: {
            baselineColor: "#ebebeb"
        }
    };
    chart.draw(data, options);
};

Analytics.prototype.setChartTitleLines = function () {
    $(".top-border-line").each(function (index, value) {
        $(value).width($(value).parent().width() - $(value).prev().outerWidth() - 10);
        $(value).show();
    });
};