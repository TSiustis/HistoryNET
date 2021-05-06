import React from 'react';
import './Home.css';

const home = (props) => {
    return (
    <>
            <h1>History Today</h1>
                    <div id = "about">
                        <h3>About</h3>
                        <p>
                            This is an API that scraped data from Wikipedia for days, storing the most important events for each day.

                            The data is fetched from a database using the API endpoints and I wrote a ReactJS interface as well.
                        </p>
                    </div>
                    <div id = "example">
                        <h3>API Usage</h3>
                        <p>
                            You can get the data for a certain day of the month at /api/events, /api/deaths or /api/births. For example a call to /api/events/getalleventsforday?day=august_30 returns the following:
                           <br/>
                            <blockquote>
                                id: 14,
                                day: "August_30",
                                url: "https://en.wikipedia.org/wiki/AD_70",
                                year: "AD 70 ",
                                html: "<a href="https://en.wikipedia.org/wiki/AD_70" title="AD 70">AD 70</a> – <a href="https://en.wikipedia.org/wiki/Titus" title="Titus">Titus</a> ends the <a href="https://en.wikipedia.org/wiki/Siege_of_Jerusalem_(70_CE)" title="Siege of Jerusalem (70 CE)">siege of Jerusalem</a> after destroying <a href="https://en.wikipedia.org/wiki/Second_Temple" title="Second Temple">Herod's Temple</a>.<sup id="cite_ref-1" class="reference"><a href="#cite_note-1">[1]</a></sup>",
                                content: "Titus ends the siege of Jerusalem after destroying Herod's Temple.[1]",
                                link: [..] ..       
</blockquote>
 
                        </p>
                        <div id = "#data">
                            <h3>Data Generation</h3>
                            <p>
                                The data will automatically start to scrape if the database is empty once the project is started. If you want to disable this comment the lines in PageScraper.cs.

                            </p>

                        </div>
                        <div id = "license">
                            <h3>License</h3>
                            <p>
                            This website and data is licensed using <a href = "https://creativecommons.org/licenses/by-sa/3.0/us/">CC BY-SA 3.0</a>, the same license used for Wikipedia data.
                            </p>
                        </div>
                    </div>
        </>
    )
}

export default home;