import { Card, Typography } from "antd";
import { useEffect, useState } from "react";
import { DndContext, useDraggable, useDroppable } from "@dnd-kit/core";

function TaskCard({ task }) {
    const { attributes, listeners, setNodeRef, transform } = useDraggable({
        id: task.Id
    });

    return (
        <div
            ref={setNodeRef}
            style={{
                padding: 8,
                background: "#eee",
                borderRadius: 6,
                marginBottom: 10,
                cursor: "grab",
                transform: transform
                    ? `translate3d(${transform.x}px, ${transform.y}px, 0)`
                    : undefined
            }}
            {...listeners}
            {...attributes}
        >
            {task.Name}
        </div>
    );
}

function Column({ column, tasks }) {
    const { setNodeRef } = useDroppable({ id: column.Id });

    return (
        <div
            ref={setNodeRef}
            style={{
                width: 250,
                padding: 10,
                background: "gray",
                borderRadius: 8
            }}
        >
            <Typography.Title level={5}>{column.Name}</Typography.Title>

            {tasks.map((task) => (
                <TaskCard key={task.Id} task={task} />
            ))}
        </div>
    );
}

export default function KanbanBoard() {
    const [columns, setColumns] = useState([]);
    const [tasks, setTasks] = useState([]);

    const updateTasks = async () => {
        const resp = await fetch("http://localhost:5002/kanban/tasks");
        const data = await resp.json();

        setTasks(data);

        const columns = new Map();
        data.forEach(t => columns.set(t.KanbanColumnId, t.KanbanColumn));

        setColumns([...columns.values()]);
    };

    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        updateTasks();
    }, []);

    function handleDragEnd(event) {
        const { active, over } = event;
        if (!over) return;

        fetch(`http://localhost:5002/kanban/move/${active.id}/${over.id}`, { method: "POST" }).then(() => {
            updateTasks();
        });

    }

    return (
        <Card>
            <Typography.Title level={4}>Kanban</Typography.Title>

            <DndContext onDragEnd={handleDragEnd}>
                <div style={{ display: "flex", gap: 20 }}>
                    {columns.map((col) => (
                        <Column column={col} tasks={tasks.filter((t) => t.KanbanColumnId === col.Id)} />
                    ))}
                </div>
            </DndContext>
        </Card>
    );
}