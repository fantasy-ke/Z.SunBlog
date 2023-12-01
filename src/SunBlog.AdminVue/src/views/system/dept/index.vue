<template>
	<div class="system-dept-container layout-padding">
		<ProTable ref="tableRef" :request-api="getTableList" :pagination="false" :columns="columns" :tool-button="false">
			<template #tools>
				<!-- v-auth="'sysorganization:add'" -->
				<el-button type="primary" icon="ele-Plus" @click="onOpenDept(null)"> 新增 </el-button>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<!-- v-auth="'sysorganization:edit'" -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpenDept(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteOrg(scope.row.id)">
					<template #reference>
						<!-- v-auth="'sysorganization:delete'"  -->
						<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<DeptDialog ref="deptDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="sysOrganization">
import { defineAsyncComponent, ref, reactive, inject } from 'vue';
import { ElMessage } from 'element-plus';

// 引入组件
const DeptDialog = defineAsyncComponent(() => import('@/views/system/dept/dialog.vue'));
import ProTable from '@/components/ProTable/index.vue';
import type { ColumnProps } from '@/components/ProTable/interface';
import { OrganizationSyssServiceProxy, UpdateOrgInput } from '@/shared/service-proxies';
const _orgSysService = new OrganizationSyssServiceProxy(inject('$baseurl'), inject('$api'));
// 定义变量内容
const deptDialogRef = ref<InstanceType<typeof DeptDialog>>();
const tableRef = ref<InstanceType<typeof ProTable>>();

const columns = reactive<ColumnProps[]>([
	{
		prop: 'name',
		label: '机构名称',
		search: { el: 'input' },
		align: 'left',
	},
	{
		prop: 'code',
		label: '机构编码',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'creationTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		width: 150,
		// isShow: auths(['sysorganization:edit', 'sysorganization:delete']),
	},
]);

// 打开新增菜单弹窗
const onOpenDept = async (row: UpdateOrgInput | null = null) => {
	await deptDialogRef.value?.openDialog(row);
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params));
	let name = (newParams.name as any) ?? '';
	return _orgSysService.getPage(name);
};

//删除机构
const onDeleteOrg = async (id: string) => {
	const { success } = await _orgSysService.delete(id);
	if (success) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped lang="scss"></style>
