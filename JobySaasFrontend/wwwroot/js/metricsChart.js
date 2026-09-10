window.metricsChart = {
    _instances: {},

    render: function (canvasId, labels, datasets) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        if (this._instances[canvasId]) {
            this._instances[canvasId].destroy();
        }

        const chartDatasets = datasets.map(ds => ({
            label: ds.label,
            data: ds.data,
            borderColor: ds.color,
            borderDash: ds.dash || [],
            fill: false,
            tension: 0,
            pointRadius: 0,
            pointHoverRadius: 4,
            borderWidth: 2
        }));

        this._instances[canvasId] = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: chartDatasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                interaction: { mode: 'index', intersect: false },
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: {
                        grid: { display: false }
                    },
                    y: {
                        beginAtZero: true,
                        grid: { color: 'rgba(137,135,129,0.15)' }
                    }
                }
            }
        });
    }
};