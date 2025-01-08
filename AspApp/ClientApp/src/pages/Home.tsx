import { Head, Link, usePage } from "@inertiajs/react";
import Layout from "@/components/shared/Layout.tsx";
import React from "react";

export interface HomeModel
{
    title: string
    time: string
}

export default function HomePage(model: HomeModel) {
    const page = usePage();
    console.log("page : ", page);
    return (
        <>
            <Head title="홈페이지"/>
            <h1 className="">홈페이지</h1>
            
            <div className={"timecode"}>
                <p>{model.time}</p>
                <Link href="/" preserveScroll
                className={"py-1 px-4 rounded rounded-md"}>Refresh</Link>
            </div>
        </>
    );
}

HomePage.layout = (page: React.ReactNode) => <Layout children={page} />;
