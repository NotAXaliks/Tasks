import { Card, Typography, Table } from "antd";
import { useEffect, useState } from "react";
import dayjs from "dayjs";

import "dayjs/locale/ru";

dayjs.locale("ru");

const DAY_COUNT = 30;

export default function GantChart() {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        fetch("http://localhost:5002/tasks").then(async (resp) => {
            const data = await resp.json();

            setTasks(data.filter((task) => {
                const date = dayjs(task.EndDate);
                
                return date.isAfter(dayjs()) || date.isSame(dayjs());
            }));
        });
    }, []);

    const today = dayjs();

    const columns = [
        {
            title: "Задача",
            dataIndex: "Name",
            fixed: "left",
            width: 200
        },
        ...Array.from({ length: DAY_COUNT }).map((_, i) => {
            return {
                title: (
                    <div
                        style={{ "writing-mode": "sideways-lr" }}
                    >
                        {today.add(i, "day").format("D MMMM")}
                    </div>
                ),
                key: i,
                width: 40,
                render: (_, task) => {
                    const start = dayjs(task.StartDate);
                    const end = dayjs(task.EndDate);

                    const startIndex = start.diff(today, "day");
                    const duration = end.diff(start, "day") + 1;

                    if (i === startIndex) {
                        return (
                            <div
                                style={{
                                    height: 20,
                                    background: "#1677ff",
                                    borderRadius: 2,
                                    width: duration * 40,
                                    position: "relative",
                                    zIndex: 2,
                                }}
                            />
                        );
                    }
                }
            };
        })
    ];

    return (
        <Card>
            <Typography.Title level={4}>Диаграмма ганта</Typography.Title>

            <Table
                bordered
                dataSource={tasks}
                columns={columns}
                pagination={false}
                scroll={{ x: DAY_COUNT * 40 + 200 }}
            />
        </Card>
    );
}