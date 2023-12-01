<template>
	<div class="blog-friendLink-container layout-padding">
		<ProTable ref="tableRef" :request-api="getTableList" :data-callback="dataCallBack" :columns="columns" :tool-button="false">
			<!-- v-auth="'friendLink:add'" -->
			<template #tools> <el-button type="primary" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #siteName="{ row }">
				<el-link target="_blank" type="primary" :underline="false" :href="row.link">{{ row.siteName }}</el-link>
			</template>
			<template #isIgnoreCheck="{ row }">
				{{ row.isIgnoreCheck ? '否' : '是' }}
			</template>
			<!-- <template #logo="{ row }">
				<el-avatar :src="row.logo" />
			</template> -->
			<template #logo="{ row }">
				<el-image shape="square" :size="100" fit="cover" :src="row.logo" />
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<!-- v-auth="'friendLink:edit'" -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpen(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<!-- v-auth="'friendLink:delete'"  -->
						<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<LinkDialog ref="linkDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="friendLink">
import { defineAsyncComponent, inject, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
// 引入组件
const LinkDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '@/components/ProTable/index.vue';
import { ColumnProps } from '@/components/ProTable/interface';
import { CreateOrUpdateFriendInput, FriendLinkPageQueryInput, FriendLinksServiceProxy } from '@/shared/service-proxies';
import moment from 'moment';
const _friendLinkService = new FriendLinksServiceProxy(inject('$baseurl'), inject('$api'));

//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const linkDialogRef = ref<InstanceType<typeof LinkDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'siteName',
		label: '站点名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'logo',
		label: 'Logo',
		width: 180,
	},
	{
		prop: 'isIgnoreCheck',
		label: '互链校验',
		width: 180,
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		// isShow: auths(['friendLink:edit', 'friendLink:delete']),
	},
]);

const dataCallBack = (data) => {
	data.rows.forEach((res) => {
		if (res.createdTime) {
			res.createdTime = moment(res.createdTime).format('YYYY-MM-DD');
		}
	});
	return data;
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as FriendLinkPageQueryInput;
	return _friendLinkService.getPage(newParams);
};
// 打开新增标签弹窗
const onOpen = (row: CreateOrUpdateFriendInput | null) => {
	linkDialogRef.value?.openDialog(row);
};

// 删除角色
const onDeleteRole = async (id: string) => {
	const { success } = await _friendLinkService.delete(id);
	if (success) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped></style>
