import { Badge, Calendar, Card, Typography } from "antd";
import { useEffect, useState } from "react";

export default function CalendarPage() {
    const [holidays, setHolidays] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5002/holidays").then(async (resp) => {
            const data = await resp.json();

            setHolidays(data);
        });
    }, []);

    return (
        <Card>
            <Typography.Title level={4}>Календарь с праздниками</Typography.Title>

            <Calendar cellRender={(date) => {
                const todayHolidays = holidays.filter((task) => {
                    const startDate = new Date(task.StartDate);

                    return startDate.getDate() === date.date() && startDate.getMonth() === date.month() && startDate.getFullYear() === date.year();
                });

                return (
                    <>
                        {todayHolidays.map((holiday) => (
                            <>
                                <Badge key={holiday.Id} color="blue" text={holiday.Name} />
                                <br />
                            </>
                        ))}
                    </>
                )
            }} />
        </Card>
    )
}
