import { Layout, Tabs } from 'antd';
import GantChart from './components/GantChart';
import CalendarPage from './components/CalendarPage';
import KanbanBoard from './components/KanbanBoard';

const items = [
  {
    key: 'calendar',
    label: 'Календарь с праздниками',
    children: <CalendarPage />,
  },
  {
    key: 'gant',
    label: 'Диаграмма ганта',
    children: <GantChart />,
  },
  {
    key: 'kanban',
    label: 'Канбан доска',
    children: <KanbanBoard />,
  },
];

export default function App() {

  return (
    <Layout style={{ minHeight: '100vh', minWidth: '100vw' }}>
      <Layout.Content style={{ padding: '24px' }}>
        <Tabs defaultActiveKey="calendar" items={items} />
      </Layout.Content>
    </Layout>
  );
}