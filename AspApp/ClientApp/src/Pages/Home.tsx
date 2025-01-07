import { Head } from "@inertiajs/react";

export interface HomeProps
{
    title: string
    time: string
}

export default function Home(model: HomeProps) {
    return (
        <div>
            <Head title={model.title} />
            <h1 className="text-blue-500">Home</h1>
            <p>{model.time}</p>
        </div>
    );
}

// IndexPage.layout = (page) => <Layout title="Home" children={page} />;
