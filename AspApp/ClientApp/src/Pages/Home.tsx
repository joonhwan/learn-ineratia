import { Head } from "@inertiajs/react";

export interface HomeProps
{
    title: string
    time: string
}

export default function Home(model: HomeProps) {
    return (
        <>
            <Head title={model.title} />
            <h1>Home</h1>
            <p>{model.time}</p>
        </>
    );
}

// IndexPage.layout = (page) => <Layout title="Home" children={page} />;
