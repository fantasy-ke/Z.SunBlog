$serviceResponse = (Get-Content -Raw -Path 'template-response.ts' )
$importMoment = "import * as moment from 'moment';"
Set-Location ../src/shared/service-proxies

$serviceProxies = (Get-Content -Raw -Path 'service-proxies.ts' )

$serviceProxiesOutput = $serviceProxies.Replace($importMoment,$serviceResponse)

Set-ItemProperty -Path 'service-proxies.ts' -Name IsReadOnly -Value $false

Set-Content -Path 'service-proxies.ts' -Value $serviceProxiesOutput
