﻿import { Head, Link } from "@inertiajs/react";

export interface Model
{
    title: string
    time: string
}

export default function View(model: Model) {
    return (
        <div>
            <Head title="홈페이지"/>
            <h1 className="text-blue-500">홈페이지</h1>
            <nav>
                <ul className={"list-disc list-inside"}>
                    <li><Link href="/">홈페이지</Link></li>
                    <li><Link href="/users">사용자정보</Link></li>
                    <li><Link href="/settings">설정</Link></li>
                    <li><Link href="/logout" method={"post"} data={{foo: "bar"}}>로그아웃</Link></li>
                </ul>
            </nav>
            <p>{model.time}</p>
        </div>
    );
}

// IndexPage.layout = (page) => <Layout title="Home" children={page} />;
