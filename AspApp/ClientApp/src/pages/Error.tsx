
export interface ErrorPageProps {
    status: number
    message?: string
}

export default function ErrorPage({ status }: ErrorPageProps) {
    const title = {
        503: '503: Service Unavailable',
        500: '500: Server Error',
        404: '404: Page Not Found',
        403: '403: Forbidden',
    }[status]

    const description = {
        503: 'Sorry, we are doing some maintenance. Please check back soon.',
        500: 'Whoops, something went wrong on our servers.',
        404: 'Sorry, the page you are looking for could not be found.',
        403: 'Sorry, you are forbidden from accessing this page.',
    }[status]

    return (
        <div className={"flex flex-col items-center justify-center min-h-dvh"}>
            <h1 className={"text-2xl font-bold mb-4"}>{title}</h1>
            <p>🫨 {description}</p>
        </div>
    )
}

// ErrorPage.layout = (page: React.ReactNode) => <Layout children={page} />;