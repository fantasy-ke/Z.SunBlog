<template>
	<div class="custom-config-item layout-padding">
		<ProTable
			v-if="state.isShow"
			ref="tableRef"
			:columns="state.columns"
			:tool-button="false"
			:init-param="state.params"
			:data-call-back="dataCallBack"
			:request-api="getTableList"
		>
			<template #tools>
				<!-- v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'" -->
				<el-button type="primary" icon="ele-Plus" @click="onOpenRender('')"> 新增 </el-button></template
			>
			<template #tempStatus="scope">
				<el-tag :type="scope.row.tempStatus === 0 ? 'success' : 'danger'"> {{ scope.row.tempStatus === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="{ row }">
				<!-- v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'" -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpenRender(row.tempId)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(row.tempId)">
					<template #reference>
						<!-- v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'" -->
						<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<RenderDialog ref="renderDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="tsx" name="customItemList">
import { ref, reactive, onMounted, nextTick, defineAsyncComponent, inject } from 'vue';
import { useRoute } from 'vue-router';
import ProTable from '@/components/ProTable/index.vue';
import type { ColumnProps } from '@/components/ProTable/interface';
import { ElMessage } from 'element-plus';
import { CustomConfigItemsServiceProxy, CustomConfigsServiceProxy, GetConfigDetailInput } from '@/shared/service-proxies';
const _customConfigService = new CustomConfigsServiceProxy(inject('$baseurl'), inject('$api'));
const _customConfigItemService = new CustomConfigItemsServiceProxy(inject('$baseurl'), inject('$api'));
const route = useRoute();
// 引入组件
const RenderDialog = defineAsyncComponent(() => import('./renderDialog.vue'));

// 表格实例
const tableRef = ref<InstanceType<typeof ProTable>>();

// 数据编辑弹窗实例
const renderDialogRef = ref<InstanceType<typeof RenderDialog>>();

// 页面数据状态
const state = reactive({
	columns: [
		{
			type: 'index',
			label: '序号',
			width: 60,
		},
	] as ColumnProps[],
	isShow: false,
	params: { id: route.query.id as never as number },
});

const dataCallBack = (result) => {
	let row = [];
	result.rows.forEach((res) => row.push(JSON.parse(res)));
	result.rows = row;
};

// 打开新增、编辑弹窗
const onOpenRender = async (itemId?: string) => {
	await renderDialogRef.value?.openDialog(route.query.id as never, itemId);
};

const getTableList = (params: any) => {
	return _customConfigItemService.getPage(params);
};

// 删除
const onDeleteRole = async (id: string) => {
	const { success, error } = await _customConfigItemService.delete(id);
	if (success) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	} else {
		ElMessage.error(error.message);
	}
};

onMounted(async () => {
	const { result } = await _customConfigService.getFormJson({ id: route.query.id } as GetConfigDetailInput);
	const json = JSON.stringify(result!.formJson);
	const reg =
		/{"key":\d+,"type":"(input|select|date|switch|number|textarea|radio|checkbox|time|time-range|date-range|rate|color|slider|cascader|rich-editor|file-upload|picture-upload)".*?"id".*?}/g;
	const optionSting = json.match(reg)?.join(',');
	if (optionSting) {
		const options: Array<any> = JSON.parse(`[${optionSting}]`);
		options
			.filter((i) => i.type !== 'rich-editor' && i.type !== 'file-upload')
			.forEach((item) => {
				let option = item.options;
				state.columns.push({
					prop: option.name,
					label: option.label,
					render: (scope) => {
						let row = scope.row;
						let v = row[option.name];
						if (v === null || v === undefined) {
							return v;
						}
						switch (item.type) {
							case 'picture-upload':
								return <el-image shape="square" size={100} fit="cover" src={option.limit == 1 ? v : v[0]} />;
							case 'select':
							case 'checkbox':
							case 'radio':
								let options = option.optionItems;
								return option.multiple || item.type === 'checkbox'
									? (options as Array<any>)
											.filter((f) => (v as Array<any>).includes(f.value))
											.map((m) => m.label)
											.join(',')
									: (options as Array<any>).find((f) => f.value.toString() === v.toString()).label;
							case 'switch':
								let type = option.label.indexOf('启') > -1 || option.label.indexOf('禁') > -1 ? ['启用', '禁用'] : ['是', '否'];
								return v ? type[0] : type[1];
							case 'time-range':
							case 'date-range':
								return (v ?? []).join('-');
							default:
								return v;
						}
					},
				});
			});
	}
	state.columns.push(
		...[
			{ label: '状态', prop: 'tempStatus', width: 80 },
			{
				width: 160,
				label: '创建时间',
				prop: 'tempCreatedTime',
			},
			{
				prop: 'action',
				label: '操作',
				width: 150,
				fixed: 'right',
			},
		]
	);
	nextTick(() => {
		state.isShow = true;
	});
});
</script>

<style scoped></style>
